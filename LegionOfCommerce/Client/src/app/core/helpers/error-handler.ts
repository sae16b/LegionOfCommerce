import { throwError, Observable } from 'rxjs';

export function serverError(err: any) {
  let errMsg = 'Server error: ';

  if (err.error) {
    errMsg += err.error;
  } else if (err.status) {
    errMsg += `${err.status}: ${err.statusText}`;
  }

  console.error(errMsg);
  return throwError('Something went wrong. Please try again later.');
}
