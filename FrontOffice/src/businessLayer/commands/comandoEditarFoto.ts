import { Comando } from './comando';
import { Injectable } from '@angular/core';
import { FabricaEntidad } from '../../dataAccessLayer/factory/fabricaEntidad';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';

@Injectable()
export class ComandoEditarFoto extends Comando {
    

    constructor(public restapiService: RestapiService, private storage: Storage ) 
    {
        super();
        
    };

    public execute() : Promise<any>
    {
        return new Promise((resolve,reject) => {
            
           })
    }

    public return() : Entidad 
    { 
        return this._entidad
    };

}