using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Data.SQL;
using Backend.Users;
using Backend.Storage;
using Backend.Storage.Hierarchical;
using Backend.Interface;
using Backend.Interface.TCP;
using Backend.ActionProviders;
using Backend.ActionProviders.Console;

namespace Backend
{
    class ProgramMain
    {

        // Program start
        static void Main(string[] args)
        {
            DataManager _dataManager = new SQLDataManager();
            FileManager _fileManager = new HierarchicalFileManager();
            UserManager _userManager = new UserManager(_dataManager, _fileManager);

            ServerInterface _interface = new TCPInterface();
            _interface.Run();


            ActionProvider _provider = new ConsoleReaderActionProvider();

            ServerAction action;
            while ((action = _provider.Dequeue()).ExitServer == false)
                if (!action.IsEmpty)
                    action.PerformAction(_userManager, _fileManager, _dataManager);


            _interface.Stop();
        }
    }
}
