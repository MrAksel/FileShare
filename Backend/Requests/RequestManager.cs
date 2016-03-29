using Backend.Data;
using Backend.Storage;
using Backend.Users;

namespace Backend.Requests
{
    internal class RequestManager
    {
        private DataManager _dataManager;
        private FileManager _fileManager;
        private UserManager _userManager;

        internal RequestManager(DataManager dataManager, FileManager fileManager, UserManager userManager)
        {
            _dataManager = dataManager;
            _fileManager = fileManager;
            _userManager = userManager;
        }


        // TODO Layout of this class. Will happen when needed
    }
}
