using CloudBasedRMS.Core;
using CloudBasedRMS.GenericRepositories;
namespace CloudBasedRMS.Services
{
    public  class ApplicationSettingServices :BaseServices
    {
        //Create instance of Interface for ApplicationSettings
        public IApplicationSettingRepository ApplicationSettings { get; private set; }
        public ApplicationSettingServices()
        {
            ApplicationSettings = unitOfWork.ApplicationSettings;
        }
        #region Paging Size
        public int PagingSize
        {
            get
            {
                ApplicationSetting pagingsize = ApplicationSettings.SingleOrDefault(x => x.Key == "PagingSize" && x.Active == true);

                if (pagingsize == null)
                {
                    return 10;
                }
                else
                {
                    int value;
                    if (int.TryParse(pagingsize.Value, out value))
                    {
                        return value;
                    }
                    else
                    {
                        return 10;
                    }
                }
            }
        }

        #endregion

        #region [DateFormat
        public string DateFormat
        {
            get
            {
                ApplicationSetting dateFormat = ApplicationSettings.SingleOrDefault(x => x.Key == "DateFormat" && x.Active == true);

                if (dateFormat == null)
                {
                    return "dd/MM/yyyy";
                }
                else
                {
                    return dateFormat.Value;
                }

            }
        }
        #endregion

        #region TimeFormat
        public string TimeFormat
        {
            get
            {
                ApplicationSetting dateFormat = ApplicationSettings.SingleOrDefault(x => x.Key == "TimeFormat" && x.Active == true);

                if (dateFormat == null)
                {
                    return "hh:mm tt";
                }
                else
                {
                    return dateFormat.Value;
                }

            }
        }
        #endregion

        #region AppName
        public string ApplicationName
        {
            get
            {
                ApplicationSetting dateFormat = ApplicationSettings.SingleOrDefault(x => x.Key == "ApplicationName" && x.Active == true);

                if (dateFormat == null)
                {
                    return "CloudBased Restaurant Management System";
                }
                else
                {
                    return dateFormat.Value;
                }

            }
        }
        #endregion

        #region App Version
        public string ApplicationVersion
        {
            get
            {
                ApplicationSetting dateFormat = ApplicationSettings.SingleOrDefault(x => x.Key == "ApplicationVersion" && x.Active == true);

                if (dateFormat == null)
                {
                    return "CBRMS v.1.0.0.0";
                }
                else
                {
                    return dateFormat.Value;
                }
            }
        }
        #endregion

        #region DefaultUserPassword
        public string DefaultUserPassword
        {
            get
            {
                ApplicationSetting dateFormat = ApplicationSettings.SingleOrDefault(x => x.Key == "DefaultUserPassword" && x.Active == true);

                if (dateFormat == null)
                {
                    return "CBRMS v.1.0.0.0";
                }
                else
                {
                    return dateFormat.Value;
                }
            }
        }
        #endregion


        #region FooterTradeMark
        public string FooterTradeMark
        {
            get
            {
                ApplicationSetting dateFormat = ApplicationSettings.SingleOrDefault(x => x.Key == "FooterTradeMark" && x.Active == true);

                if (dateFormat == null)
                {
                    return "Copyright";
                }
                else
                {
                    return dateFormat.Value;
                }
            }
        }
        #endregion
        
    }
}
