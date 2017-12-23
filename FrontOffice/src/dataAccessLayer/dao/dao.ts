import { Entidad } from '../domain/entidad';

export abstract class DAO{
    
    abstract agregar(entidad : Entidad) : Entidad;


    abstract visualizar(entidad : Entidad) : Entidad;

    

    abstract eliminar(entidad : Entidad) : boolean;

     

    abstract modificar(entidad : Entidad) : boolean;

    


}