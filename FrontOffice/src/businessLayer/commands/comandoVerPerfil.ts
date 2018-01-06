import { Comando } from './comando';
import { TranslateService } from '@ngx-translate/core';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { catProd, catService, catErr } from '../../logs/config';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import { Usuario } from '../../dataAccessLayer/domain/usuario';
import { Storage } from '@ionic/storage';
import { Injectable } from '@angular/core';
import { FabricaEntidad } from '../../dataAccessLayer/factory/fabricaEntidad';

@Injectable()
export class ComandoVerPerfil extends Comando {
    

    constructor( private storage: Storage, private translateService: TranslateService, public restapiService: RestapiService ) 
    {
        super();
        
    };

    public execute() : Promise<null>
    {   
        return new Promise((resolve) => {
            // obtenemos el id ya almacenado desde el login
            this.storage.get('id').then((val) => {
                this._entidad.setId = val;
                // hacemos la llamada al apirest con el id obtenido
                this.restapiService.ObtenerDatosUsuario(this._entidad.getId).then(data => {
                    this._entidad = FabricaEntidad.crearUsuarioConParametros(data.id, data.Nombre, data.Apellido, data.Correo, data.NombreUsuario, data.FechaNacimiento);
                    // cargamos el idioma
                    this.storage.get(this._entidad.getId.toString()).then((val) => {
                        //verificamos que posee configuracion previa de idioma
                        if (val != null || val != undefined) {
                            this.translateService.use(val);
                        }
                    });
                    
                    resolve();
                    
                }, error => {
                   
                    console.log('falla en la carga de los datos');
                    resolve();
                })
            });

        })
    
    }

    public return() : Entidad 
    { 
        return this._entidad
    };

}