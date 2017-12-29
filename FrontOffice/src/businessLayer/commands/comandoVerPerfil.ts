import { Comando } from './comando';
import { TranslateService } from '@ngx-translate/core';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { catProd, catService, catErr } from '../../logs/config';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import { Usuario } from '../../dataAccessLayer/domain/usuario';

export class ComandoVerPerfil extends Comando {

    
    constructor( private usuario : Usuario, public restapiService: RestapiService, private storage: Storage, private translateService: TranslateService ) 
    {
        super();
        this._entidad = usuario;
    };

    public execute() 
    {
        this.storage.get('id').then((val) => {

            this._entidad.Id = val;
            // hacemos la llamada al apirest con el id obtenido
            this.restapiService.ObtenerDatosUsuario(this.usuario.Id).then(data => {
              if(data != 0)
              {  
                this._entidad = data as Entidad;
                //this.usuario.Id = this.idUsuario; 
      
                    // cargamos el idioma
                    this.storage.get(this._entidad.Id.toString()).then((val) => {
                      //verificamos que posee configuracion previa de idioma
                      if(val != null || val != undefined){
                        this.translateService.use(val);
                      }
                    });
      
                    // cargamos los datos para la vista de configuracion
                   // this.configParams.idUsuario = this.idUsuario;
                   // this.configParams.NombreUsuario = this.usuario.NombreUsuario;
              }
            });
        });
    }

    public isSuccess() : boolean 
    {
        return
    };
    public return() {};

}