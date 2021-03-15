import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class HttpAuthInterceptor implements HttpInterceptor {

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const authToken = localStorage.getItem("authToken");

    if (authToken) {
        const cloned = request.clone({
            headers: request.headers.set("Authorization", "Bearer " + authToken)
        });

        return next.handle(cloned);
    }
    else {
        return next.handle(request);
    }
  }
}
