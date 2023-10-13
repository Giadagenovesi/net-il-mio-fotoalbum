using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;
using net_il_mio_fotoalbum.Models.Db_Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "ADMIN,USER")]
    public class ImageController : Controller
    {
        private PhotoBookContext _myDatabase;
        public ImageController(PhotoBookContext db)
        {
            _myDatabase = db;
        }
        public IActionResult Index()
        {
            List<Image> images = _myDatabase.Images.Include(image => image.Categories).ToList<Image>();
            return View("Index", images);
        }

        //SEARCH
        public IActionResult Filter(string userSearch)
        {
            List<Image> images;

            if (!string.IsNullOrEmpty(userSearch))
            {
                images = _myDatabase.Images.Where(image => image.Title.Contains(userSearch)).Include(image => image.Categories).ToList();
            } else
            {
                images = _myDatabase.Images.Include(image => image.Categories).ToList();
            }
            return View("Index", images);
        }
        //READ
        public IActionResult Details(int id)
        {
            Image? singleImage = _myDatabase.Images.Where(image => image.Id == id).Include(image => image.Categories).FirstOrDefault();

            if (singleImage == null)
            {
                return View("NotFoundPage");
            }
            else
            {
                return View("Details", singleImage);
            }
        }

        //CREATE
        [HttpGet]
        public IActionResult Add()
        {
            List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
            List<Category> dbAllCategories = _myDatabase.Categories.ToList();

            foreach (Category singleCategory in dbAllCategories)
            {
                allCategoriesSelectList.Add(new SelectListItem
                {
                    Text = singleCategory.Title,
                    Value = singleCategory.Id.ToString(),
                });
            }

            ImageFormModel model =
                new ImageFormModel { Image = new Image(), Categories = allCategoriesSelectList };
            return View("Add", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ImageFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
                List<Category> dbAllCategories = _myDatabase.Categories.ToList();

                foreach (Category singleCategory in dbAllCategories)
                {
                    allCategoriesSelectList.Add(new SelectListItem
                    {
                        Text = singleCategory.Title,
                        Value = singleCategory.Id.ToString(),
                    });
                }

                data.Categories = allCategoriesSelectList;

                return View("Add", data);
            }

            data.Image.Categories = new List<Category>();

            if (data.SelectedCategoriesId != null)
            {
                foreach (string selectedId in data.SelectedCategoriesId)
                {
                    int intCategoriesId = int.Parse(selectedId);

                    Category? categoryInDb = _myDatabase.Categories.Where(category => category.Id == intCategoriesId).FirstOrDefault();

                    if (categoryInDb != null)
                    {
                        data.Image.Categories.Add(categoryInDb);
                    }
                }
            }

            this.SetImageFileFromFile(data);

            _myDatabase.Images.Add(data.Image);
            _myDatabase.SaveChanges();

            return RedirectToAction("Index");
        }


        //UPDATE
        [HttpGet]
        public IActionResult Update(int id)
        {
            Image? imageToChange = _myDatabase.Images.Where(pizza => pizza.Id == id).Include(pizza => pizza.Categories).FirstOrDefault();

            if (imageToChange == null)
            {
                return View("NotFoundPage");
            }
            else
            {
                

                List<Category> dbCategoryList = _myDatabase.Categories.ToList();
                List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();

                foreach (Category category in dbCategoryList)
                {
                    allCategoriesSelectList.Add(new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Title,
                        Selected = imageToChange.Categories.Any(Category => category.Id == category.Id)
                    });
                }


                ImageFormModel model
                    = new ImageFormModel { Image = imageToChange,Categories = allCategoriesSelectList };

                return View("Update", model);
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, ImageFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> dbCategoryList = _myDatabase.Categories.ToList();
                List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();

                foreach (Category singleCategory in dbCategoryList)
                {
                    allCategoriesSelectList.Add(new SelectListItem
                    {
                        Value = singleCategory.Id.ToString(),
                        Text = singleCategory.Title
                    });
                }

                data.Categories = allCategoriesSelectList;

                return View("Update", data); ;
            }

            Image? imageToUpdate = _myDatabase.Images.Where(image => image.Id == id).Include(image => image.Categories).FirstOrDefault();

            if (imageToUpdate != null)
            {
                imageToUpdate.Categories.Clear();
                imageToUpdate.ImageUrl = data.Image.ImageUrl;
                imageToUpdate.Title = data.Image.Title;
                imageToUpdate.Description = data.Image.Description;
                



                if (data.SelectedCategoriesId != null)
                {
                    foreach (string categorySelectedId in data.SelectedCategoriesId)
                    {
                        int intCategorySelectedId = int.Parse(categorySelectedId);

                        Category? categoryInDb = _myDatabase.Categories.Where(category => category.Id == intCategorySelectedId).FirstOrDefault();

                        if (categoryInDb != null)
                        {
                            imageToUpdate.Categories.Add(categoryInDb);
                        }
                    }
                }

                if (data.ImageFormFile != null)
                {
                    MemoryStream stream = new MemoryStream();
                    data.ImageFormFile.CopyTo(stream);
                    imageToUpdate.ImageFile = stream.ToArray(); 
                }

                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View("NotFoundPage");
            }

        }



        //DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Image? imageToDelete = _myDatabase.Images.Where(image => image.Id == id).FirstOrDefault();

            if (imageToDelete != null)
            {
                _myDatabase.Images.Remove(imageToDelete);
                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View("NotFoundPage");
            }
        }


        //Metodo per gestione file immagini
        private void SetImageFileFromFile(ImageFormModel formData)
        {
            if(formData.ImageFormFile == null)
            {
                return;
            }

            MemoryStream stream = new MemoryStream();
            formData.ImageFormFile.CopyTo(stream);
            formData.Image.ImageFile = stream.ToArray();
        }
    }
}
