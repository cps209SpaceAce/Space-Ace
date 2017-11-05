using System.IO;

interface ISavable {
    public string Save();
}

public class SaveManager {
    StreamWriter writer;
    StreamReader reader;

    public void Save() {
        //TODO: run for loop and collect all strings generated from each object in world
        //format of string: TypeOfEntity,instance variable1, instance variable2, instance variable3, instance variable4 ... so on
        //                  TypeOfEntity,instance variable1, instance variable2, instance variable3, instance variable4 ... so on

        //TODO: Write each line to file, leave GameController class at end                
    }

    public GameController Load() {
        //TODO: make check if file exists, if no file exists, create world with default settings

        //TODO: read from file and create entities with the specifications found in the file

        //Returns new GameController class with all the specifications in the file
        return new GameController();
    }
}