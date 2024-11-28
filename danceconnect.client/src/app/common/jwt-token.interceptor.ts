import { HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class JwtTokenInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //var currentUser = JSON.parse(localStorage.getItem('currentUser') ?? "");

    var currentUser = localStorage.getItem('currentUser');

    if (currentUser) {
      var parsedUser = JSON.parse(currentUser);
      if (parsedUser && parsedUser.token) {
        req = req.clone({
          setHeaders: {
            Authorization: `Bearer ${parsedUser.token.value}`
          }
        });
      }
    }
    console.log("Request", req);
    return next.handle(req);
  }
};


//intercept(request: HttpRequest<any>, next: HttpHandler): Observable < HttpEvent < any >> {
//  // add authorization header with jwt token if available
//  let currentUser = this._authService.currentUserValue;
//  if(currentUser && currentUser.token) {
//  request = request.clone({
//    setHeaders: {
//      Authorization: `Bearer ${currentUser.token}`
//    }
//  });
//}

//return next.handle(request);
//    }
