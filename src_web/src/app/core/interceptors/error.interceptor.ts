import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = "Wystapil błąd";
      
      if (error.error instanceof ErrorEvent) {
        errorMessage = `Błąd: ${error.error.message}`;
      }
      else {
        // po stronie serwera
        if (error.error && error.error.detail) {
             errorMessage = error.error.detail;
        } else if (error.error && error.error.title) {
            errorMessage = error.error.title;
        } else {
            errorMessage = `Status błędu: ${error.status}\n wiadomość błędu: ${error.message}`;
        }
      }
      
      console.error('Błąd żądania http:', errorMessage);
      console.log('todo: toastr');
      
      return throwError(() => error);
    })
  );
};
