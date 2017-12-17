import { DAOChat } from '../../dataAccessLayer/dao/daoChat';
import { DAOChatGrupo } from '../../dataAccessLayer/dao/daoChatGrupo';

export class FabricaDAO{
    

    public static crearFabricaDAOChat(){
        return new DAOChat();
    }

    public static crearFabricaDAOChatGrupo(){
        return new DAOChatGrupo();
    }

}