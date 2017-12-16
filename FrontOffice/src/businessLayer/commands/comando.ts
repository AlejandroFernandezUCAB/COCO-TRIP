import { Entidad } from '../../dataAccessLayer/domain/entidad';

export abstract class Comando {
    
    _entidad : Entidad;

    
    public execute(): void {
        console.log("ENTRE EN COMANDO SOLO");
    }
}