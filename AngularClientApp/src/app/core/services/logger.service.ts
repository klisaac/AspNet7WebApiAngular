import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoggerService {

  constructor() { }

  log(msg: string) {
    if (environment.production) {
        // AppInsights
    }
    else if (environment.staging) {
      //console.log(msg);
    }
    else {
        //console.log(msg);
    }
  }

  logError(msg: string) {
      if (!environment.production) {
          //console.error(msg);
      }
      else {
          // AppInsights
      }
  }

}
