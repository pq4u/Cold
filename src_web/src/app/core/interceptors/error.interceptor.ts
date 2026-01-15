import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';

// Since I haven't installed toastr, I'll use console and maybe a simple alert or just rethrow for components to handle.
// Actually, I should probably install ngx-toastr or similar for nice notifications, but for now I'll stick to basic handling.

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = 'An unknown error occurred!';
      
      if (error.error instanceof ErrorEvent) {
        // Client-side error
        errorMessage = `Error: ${error.error.message}`;
      } else {
        // Server-side error
        if (error.error && error.error.detail) {
             errorMessage = error.error.detail;
        } else if (error.error && error.error.title) {
            errorMessage = error.error.title;
        } else {
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
      }
      
      console.error('HTTP Error:', errorMessage);
      // Here we could inject a notification service to display the error
      
      return throwError(() => error);
    })
  );
};
