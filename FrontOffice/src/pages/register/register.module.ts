
import { IonicPageModule } from 'ionic-angular';
import { RegisterPage } from './register';


import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
import { SplashScreen } from '@ionic-native/splash-screen';
import { StatusBar } from '@ionic-native/status-bar';
 
import { CocoTrip } from '../../app/app.component';
 
import { File } from '@ionic-native/file';
import { Transfer } from '@ionic-native/transfer';
import { FilePath } from '@ionic-native/file-path';
import { Camera, CameraOptions } from '@ionic-native/camera';

@NgModule({
  declarations: [
    RegisterPage,
    CocoTrip,
  ],
  imports: [
    IonicPageModule.forChild(RegisterPage),
    BrowserModule,
    IonicModule.forRoot(CocoTrip),
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    RegisterPage,
    CocoTrip,
  ],
  providers: [
    StatusBar,
    SplashScreen,
    File,
    Transfer,
    Camera,
    FilePath,
    {provide: ErrorHandler, useClass: IonicErrorHandler}
  ]
})
export class RegisterPageModule {}
