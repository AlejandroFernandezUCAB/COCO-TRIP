import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';

import { CocoTrip } from './app.component';
import { HomePage } from '../pages/home/home';
import { ListPage } from '../pages/list/list';
import { LoginPage } from '../pages/login/login';
import { PerfilPage } from '../pages/perfil/perfil';
import { AmistadesGruposPage } from '../pages/amistades-grupos/amistades-grupos';
import { EventosActividadesPage } from '../pages/eventos-actividades/eventos-actividades';
import { ItinerarioPage } from '../pages/itinerario/itinerario';
import { ChatPage } from '../pages/chat/chat';
import { Facebook } from '@ionic-native/facebook'
import { CloudSettings, CloudModule } from '@ionic/cloud-angular';
import { RegisterPage } from '../pages/register/register';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';


const cloudSettings: CloudSettings = {
  'core': {
    'app_id': 'abd7650b'
  },
  'auth': {
    'google': {
      'webClientId': '383153901052-cr1p712s23p1ddej9ibhjmh17mnud0ff.apps.googleusercontent.com',
      'scope': ['permission1', 'permission2']
    }
  }
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
    RegisterPage
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot(CocoTrip),
    CloudModule.forRoot(cloudSettings),
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
    RegisterPage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    Facebook
  ]
})
export class AppModule {}
