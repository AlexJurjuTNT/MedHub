import {Injectable} from '@angular/core';
import notify from 'devextreme/ui/notify';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  show(message: string, type: 'success' | 'error' | 'warning' | 'info', duration: number = 3000) {
    notify({
      message: message,
      position: {
        my: 'top center',
        at: 'top center',
        of: window,
        offset: '0 20'
      },
      width: 300,
      shading: false,
      animation: {
        show: {type: 'fade', duration: 400, from: 0, to: 1},
        hide: {type: 'fade', duration: 400, from: 1, to: 0}
      }
    }, type, duration);
  }

  success(message: string, duration: number = 3000) {
    this.show(message, 'success', duration);
  }

  error(message: string, duration: number = 3000) {
    this.show(message, 'error', duration);
  }

  warning(message: string, duration: number = 3000) {
    this.show(message, 'warning', duration);
  }

  info(message: string, duration: number = 3000) {
    this.show(message, 'info', duration);
  }
}
