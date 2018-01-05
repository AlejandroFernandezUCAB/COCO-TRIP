import { Comando } from './comando';
import { Injectable } from '@angular/core';
import { FabricaEntidad } from '../../dataAccessLayer/factory/fabricaEntidad';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';

@Injectable()
export class ComandoBorrarCuenta extends Comando {
    

    constructor(public restapiService: RestapiService, private storage: Storage ) 
    {
        super();
        
    };

    public execute() : Promise<any>
    {
        return new Promise((resolve,reject) => {
            this.restapiService.borrarUser(this._entidad).then(data =>{
                if(data != 0) {
                    resolve(data);
                }
                else {
                    reject();
                    
                }
            }, error => {
                console.log('error en comando borrar cuenta')
                reject();
            }
        )})
    }

    public return() : Entidad 
    { 
        return this._entidad
    };

}