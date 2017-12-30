import { Comando } from './comando';
import { TranslateService } from '@ngx-translate/core';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { catProd, catService, catErr } from '../../logs/config';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import { Usuario } from '../../dataAccessLayer/domain/usuario';
import { Storage } from '@ionic/storage';

export class ComandoVerPerfil extends Comando {

    private exito: boolean;
    
    constructor( private entidad : Entidad, private storage: Storage, private translateService: TranslateService, public restapiService: RestapiService ) 
    {
        super();
        this._entidad = entidad;
    };

    public execute() : Promise<null>
    {   
        return new Promise((resolve => {
            // obtenemos el id ya almacenado desde el login
            this.storage.get('id').then((val) => {
                this._entidad.Id = val;
                // hacemos la llamada al apirest con el id obtenido
                this.restapiService.ObtenerDatosUsuario(this._entidad.Id).then(data => {
                     
                    this._entidad = data as Entidad;
                    //this.usuario.Id = this.idUsuario; 
                    // cargamos el idioma
                    this.storage.get(this._entidad.Id.toString()).then((val) => {
                        //verificamos que posee configuracion previa de idioma
                        if (val != null || val != undefined) {
                            this.translateService.use(val);
                        }
                    });
                    this.exito =true;
                    // cargamos los datos para la vista de configuracion
                    // this.configParams.idUsuario = this.idUsuario;
                    // this.configParams.NombreUsuario = this.usuario.NombreUsuario;
                    resolve();
                    
                }, error => {
                    this.exito = false;
                    resolve();
                })
            });

        }))
    
    }

    public isSuccess() : boolean 
    {
        return this.exito;
    };
    public return() : Entidad 
    { 
        return this._entidad
    };

}