using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utility.Models;

namespace Utility
{
    public class Config
    {
        #region Global
        private static XElement Root
        {
            get
            {
                return XDocument.Load(Define.ConfigFile).Root;
            }
        }

        public static Dictionary<string,string> SystemName
        {
            get
            {
                var element = Root.Element("SystemName");
                return new Dictionary<string, string>()
                {
                    {"zh-cn",element.Attribute("zh-cn").Value },
                    {"us-en",element.Attribute("us-en").Value }
                };
            }
        }

        public static string LogFolder
        {
            get
            {
                return Root.Element("Folder").Element("Log").Value;
            }
        }
        #endregion

        #region LDAP
        public static bool HaveLDAPSettings
        {
            get
            {
                return Root.Element("LDAP") != null;
            }
        }

        public static string LDAP_Domain
        {
            get
            {
                return Root.Element("LDAP").Attribute("Domain").Value;
            }
        }

        public static string LDAP_LoginId
        {
            get
            {
                return Root.Element("LDAP").Attribute("LoginId").Value;
            }
        }

        public static string LDAP_Password
        {
            get
            {
                return Root.Element("LDAP").Attribute("Password").Value;
            }
        }
        #endregion

        #region UserPhoto
        public static string UserPhotoFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("UserPhoto").Value;
            }
        }

        public static string UserPhotoVirtualPath
        {
            get
            {
                return Root.Element("Floder").Element("UserPhoto").Attribute("VirtualPath").Value;
            }
        }
        #endregion

        public static List<PopulationLimit> PopulationLimits
        {
            get
            {
                var populationLimits = new List<PopulationLimit>();

                try
                {
                    var elements = Root.Element("PopulationLimits").Elements("PopulationLimit").ToList();

                    foreach(var element in elements)
                    {
                        populationLimits.Add(new PopulationLimit()
                        {
                            OrganizationId = new Guid(element.Attribute("OrganizationId").Value),
                            NumberOfPeople = int.Parse(element.Attribute("NumberOfPeople").Value),
                            NumberOfMobilePeople = int.Parse(element.Attribute("NumberOfMobilePeople").Value)
                        });
                    }
                }
                catch
                {
                    populationLimits = new List<PopulationLimit>();
                }

                return populationLimits;
            }
        }

        public static string MaintenancePhotoFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("Maintenance").Element("Photo").Value;
            }
        }

        public static string CheckSQLiteGeneratedFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("Check").Element("SQLite").Element("Generated").Value;
            }
        }

        public static string EquipmentMaintenanceMobileReleaseFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("EquipmentMaintenance").Element("MobileRelease").Value;
            }
        }

        public static string CheckSQLiteTemplateFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("Check").Element("SQLite").Element("Template").Value;
            }
        }        

        public static string CheckSQLiteUploadFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("Check").Element("SQLite").Element("Upload").Value;
            }
        }

        public static string CheckSQLiteProcessingFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("Check").Element("SQLite").Element("Processing").Value;
            }
        }

        public static string CheckSQLiteBackupFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("Check").Element("SQLite").Element("Backup").Value;
            }
        }

        public static string CheckSQLiteErrorFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("Check").Element("SQLite").Element("Error").Value;
            }
        }

        public static string CheckPhotoFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("Check").Element("Photo").Value;
            }
        }

        public static string CheckFileFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("Check").Element("File").Value;
            }
        }

        public static string EquipmentMaintenanceTagSQLiteTemplateFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("EquipmentMaintenance").Element("TagSQLite").Element("Template").Value;
            }
        }

        public static string EquipmentMaintenanceTagSQLiteGeneratedFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("EquipmentMaintenance").Element("TagSQLite").Element("Generated").Value;
            }
        }

        public static string EquipmentMaintenanceTagSQLiteUploadFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("EquipmentMaintenance").Element("TagSQLite").Element("Upload").Value;
            }
        }

        public static string EquipmentMaintenanceTagSQLiteProcessingFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("EquipmentMaintenance").Element("TagSQLite").Element("Processing").Value;
            }
        }

        public static string EquipmentMaintenanceTagSQLiteBackupFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("EquipmentMaintenance").Element("TagSQLite").Element("Backup").Value;
            }
        }

        public static string EquipmentMaintenanceTagSQLiteErrorFolderPath
        {
            get
            {
                return Root.Element("Folder").Element("EquipmentMaintenance").Element("TagSQLite").Element("Error").Value;
            }
        }
    }
}
