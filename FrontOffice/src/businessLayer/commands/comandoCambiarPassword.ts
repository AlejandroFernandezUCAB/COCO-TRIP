import { Comando } from './comando';
import { Injectable } from '@angular/core';
import { FabricaEntidad } from '../../dataAccessLayer/factory/fabricaEntidad';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';
import {catService,catProd} from "../../logs/config"

@Injectable()
export class ComandoCambiarPassword extends Comando {
    
    private contrasenaActual: string;

    constructor(public restapiService: RestapiService, private storage: Storage ) 
    {
        super();
        
    };

    public execute() : Promise<any>
    {
        return new Promise((resolve,reject) => {
            
            this.restapiService.cambiarPass(this._entidad, this.contrasenaActual).then(data =>{
                if(data != 0) {
                    catProd.info("Se cambio la contraseña del Usuario  "+data);
                    resolve(data);
                }
                else {
                    reject();
                      
                }
            }, error => {
                catProd.info("Ocurrio un error cambiando la contraseña del usuario");
                console.log('error en comando cambiar contrasena', error)
                reject();
            }
        )});
    }

    public return() : Entidad 
    { 
        return this._entidad
    };

    set setContrasenaActual(contrasena : string) {
        this.contrasenaActual = contrasena;
    }

}