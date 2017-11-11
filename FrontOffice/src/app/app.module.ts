import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { Facebook } from '@ionic-native/facebook'
import { CloudSettings, CloudModule } from '@ionic/cloud-angular';
import { CocoTrip } from './app.component';

import { HomePage } from '../pages/home/home';
import { ListPage } from '../pages/list/list';
import { LoginPage } from '../pages/login/login';
import { PerfilPage } from '../pages/perfil/perfil';
import { AmistadesGruposPage } from '../pages/amistades-grupos/amistades-grupos';
import { EventosActividadesPage } from '../pages/eventos-actividades/eventos-actividades';
import { ItinerarioPage } from '../pages/itinerario/itinerario';
import { ChatPage } from '../pages/chat/chat';
import { ConversacionPage } from '../pages/chat/conversacion/conversacion';
import { RegisterPage } from '../pages/register/register';
import { EditProfilePage } from '../pages/edit-profile/edit-profile';
import { PreferenciasPage } from "../pages/preferencias/preferencias";
import { CalendarioPage } from '../pages/calendario/calendario';
import { ConfigPage } from '../pages/config/config';
import { BorrarCuentaPage } from '../pages/borrar-cuenta/borrar-cuenta';
import { ChangepassPage } from '../pages/changepass/changepass';

import { IonicStorageModule } from '@ionic/storage';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';

const cloudSettings: CloudSettings = {
  'core': {
    'app_id': 'abd7650b'
  },
  'auth': {
    'google': {
      'webClientId': '383153901052-8u7q4i0as9thogb2im0bu8ut7u52l2ud.apps.googleusercontent.com'
    }
  }
}

import { AmigosPage } from '../pages/amistades-grupos/amigos/amigos';
import { GruposPage } from '../pages/amistades-grupos/grupos/grupos';
import { NotificacionesPage } from '../pages/amistades-grupos/notificaciones/notificaciones';

import { VisualizarPerfilPage } from '../pages/VisualizarPerfil/VisualizarPerfil';
import { VisualizarPerfilPublicoPage } from '../pages/visualizarperfilpublico/visualizarperfilpublico';

import { CrearGrupoPage } from '../pages/crear-grupo/crear-grupo';

import { SeleccionarIntegrantesPage } from '../pages/seleccionar-integrantes/seleccionar-integrantes';
import { DetalleGrupoPage } from '../pages/detalle-grupo/detalle-grupo';
import { CalendarModule } from "ion2-calendar";
import { EventosCalendarioService } from '../services/eventoscalendario'
import { BuscarAmigoPage } from '../pages/buscar-amigo/buscar-amigo';
import { ModificarGrupoPage } from '../pages/modificar-grupo/modificar-grupo';
import { NuevosIntegrantesPage } from '../pages/nuevos-integrantes/nuevos-integrantes';
import { Firebase } from '@ionic-native/firebase';
import { RestapiService } from '../providers/restapi-service/restapi-service';
import { HttpCProvider } from '../providers/http-c/http-c';

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    CocoTrip,
    HomePage,
    ListPage,
    LoginPage,
    PerfilPage,
    AmistadesGruposPage,
    EventosActividadesPage,
    ItinerarioPage,
    ChatPage,
    ConversacionPage,
    AmigosPage,
    GruposPage,
    NotificacionesPage,
    VisualizarPerfilPage,
    VisualizarPerfilPublicoPage,
    CrearGrupoPage,
    SeleccionarIntegrantesPage,
    DetalleGrupoPage,
    BuscarAmigoPage,
    RegisterPage,
    ChatPage,
    EditProfilePage,
    ChangepassPage,
    ConfigPage,
    BorrarCuentaPage,
    PreferenciasPage,
    CalendarioPage,
    ModificarGrupoPage,
    NuevosIntegrantesPage
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    IonicModule.forRoot(CocoTrip),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (createTranslateLoader),
        deps: [HttpClient]
      }
    }),
    CloudModule.forRoot(cloudSettings),
    CalendarModule,
    HttpModule,
    IonicStorageModule.forRoot({
      name: 'cocotrip',
         driverOrder: ['indexeddb', 'sqlite', 'websql']
    })
  ],
  
  bootstrap: [IonicApp],
  entryComponents: [
    CocoTrip,
    HomePage,
    ListPage,
    LoginPage,
    PerfilPage,
    AmistadesGruposPage,
    EventosActividadesPage,
    ItinerarioPage,
    ChatPage,
    ConversacionPage,
    AmigosPage,
    GruposPage,
    NotificacionesPage,
    VisualizarPerfilPage,
    VisualizarPerfilPublicoPage,
    CrearGrupoPage,
    SeleccionarIntegrantesPage,
    DetalleGrupoPage,
    BuscarAmigoPage,
    RegisterPage,
    EditProfilePage,
    ChangepassPage,
    ConfigPage,
    BorrarCuentaPage,
    PreferenciasPage,
    CalendarioPage,
    ModificarGrupoPage,
    NuevosIntegrantesPage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    Facebook,
    EventosCalendarioService,
    Firebase,
    RestapiService,
    HttpCProvider
  ]
})
export class AppModule {}
