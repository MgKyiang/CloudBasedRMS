using CloudBasedRMS.Core;
using CloudBasedRMS.GenericRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace CloudBasedRMS.Services
{
  public  class RestaurantProfileServices:BaseServices{
        public IRestaurantProfileRepository RestaurantProfile { get; set; }
        public RestaurantProfileServices(){
            RestaurantProfile = unitOfWork.RestaurantProfile;
        }
        public bool SaveToDb(HttpPostedFileBase LogoFile, RestaurantProfile restaurantProfile){
            try
            {
                restaurantProfile.Logo = ConvertToBytes(LogoFile);
                RestaurantProfile.Add(restaurantProfile);
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw new Exception("An Error Occur When data save into database.");
            }
        }

        private byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            byte[] ImageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            ImageBytes = reader.ReadBytes((int)file.ContentLength);
            return ImageBytes;
        }
    }
}
