import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable()
export class JwtTokenInterceptor implements HttpInterceptor {
  constructor(private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //var currentUser = JSON.parse(localStorage.getItem('currentUser') ?? "");

    var currentUser = localStorage.getItem('currentUser');

    if (currentUser) {
      var parsedUser = JSON.parse(currentUser);
      if (parsedUser && parsedUser.token) {
        req = req.clone({
          setHeaders: {
            Authorization: `Bearer ${parsedUser.token}`
          }
        });
      }
    }
    console.log("Request", req);
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          console.error('401 Unauthorized - Redirecting to login.');
          this.router.navigate(['/auth/login']);
        }

        // Propagate other errors
        return throwError(() => error);
      })
    );
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
