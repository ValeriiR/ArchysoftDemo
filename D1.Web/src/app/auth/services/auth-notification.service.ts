import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AuthNotice } from 'src/app/auth/models/auth-notice.model';

@Injectable({
  providedIn: 'root'
})
export class AuthNotificationService {

  onNoticeChanged$: BehaviorSubject<AuthNotice>;

  constructor() {
      this.onNoticeChanged$ = new BehaviorSubject(null);
  }

  notify(message: string, type?: string) {
      const notice: AuthNotice = {
          message: message,
          type: type
      };

      this.onNoticeChanged$.next(notice);
  }
}
