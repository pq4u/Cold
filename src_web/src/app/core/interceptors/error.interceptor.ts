import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = "wystapil błąd";
      
      if (error.error instanceof ErrorEvent) {
        errorMessage = `błąd: ${error.error.message}`;
      }
      else {
        // po stronie serwera
        if (error.error && error.error.detail) {
             errorMessage = error.error.detail;
        } else if (error.error && error.error.title) {
            errorMessage = error.error.title;
        } else {
            errorMessage = `status błędu: ${error.status}\nwiadomość błędu: ${error.message}`;
        }
      }
      
      console.error('błąd żądania http:', errorMessage);
      console.log('todo: toastr');
      
      return throwError(() => error);
    })
  );
};
