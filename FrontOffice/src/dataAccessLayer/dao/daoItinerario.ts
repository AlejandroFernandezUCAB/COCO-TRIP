import { Mensaje } from '../domain/mensaje';
import { Entidad } from '../domain/entidad';
import { DAO } from './dao';
import { ChatProvider } from './../../providers/chat/chat';
import firebase from 'firebase';
import { Events } from 'ionic-angular';
import {catService,catProd} from "../../logs/config"
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import {HttpCProvider} from "../../providers/http-c/http-c";
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Itinerario } from '../domain/itinerario';
import { Http } from '@angular/http';
import {RequestOptions, Request, RequestMethod} from '@angular/http';
import 'rxjs/add/operator/map';

//****************************************************************************************************//
//**********************************Dao itinerario de MODULO 5*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * Dao chat
 * 
 */
@Injectable()
export class DAOItinerario extends DAO {
   private entidad:Entidad; 
   private peticion:HttpCProvider;
   public restapiService: RestapiService;
   public readonly apiUrl : string = 'http://localhost:8082/api';
/**
 * Descripcion del metodo:
 * Metodo que se encarga de agregar un nuevo itinerario 
 * 
 */
public constructor()
{
 super();
}
agregar(itinerario: Entidad) {
    return this.entidad;
}
visualizar(entidad: Entidad, evento: Events): Entidad {
    throw new Error("Method not implemented.");
}
eliminar(entidad: Entidad): boolean {
    throw new Error("Method not implemented.");
}
modificar(entidad: Entidad): boolean {
    throw new Error("Method not implemented.");
}

}