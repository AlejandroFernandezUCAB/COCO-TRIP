import { Entidad } from '../domain/entidad';

export abstract class DAO{
    
    abstract agregar(entidad : Entidad) : Entidad;


    abstract visualizar() : Entidad;

    

    abstract eliminar() : Entidad;

     

    abstract modificar() : Entidad;

    


}