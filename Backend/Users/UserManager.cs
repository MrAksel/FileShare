using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Storage;

namespace Backend.Users
{
    class UserManager
    {
        private DataManager dataManager;
        private FileManager fileManager;

        public UserManager(DataManager dataManager, FileManager fileManager)
        {
            this.dataManager = dataManager;
            this.fileManager = fileManager;
        }
    }
}
