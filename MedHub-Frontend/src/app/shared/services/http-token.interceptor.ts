import {HttpInterceptorFn} from '@angular/common/http';

export const httpTokenInterceptor: HttpInterceptorFn = (req, next) => {

  const token: string = localStorage.getItem('token') ?? '';

  req = req.clone({
    setHeaders: {
      Authorization: token ? `Bearer ${token}` : '',
    }
  })

  return next(req);
};
