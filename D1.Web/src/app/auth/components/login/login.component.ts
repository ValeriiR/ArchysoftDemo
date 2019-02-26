import { Component, OnInit } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { AuthService } from '../../services/auth.service';
import { ApiResponse } from 'src/app/shared/models/api-response.model';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthNotificationService } from 'src/app/auth/services/auth-notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  model: LoginModel = {login: 'admin@email.com', password: 'admin', remeberMe: false};
  loading = false;

  constructor(
    private translateService: TranslateService,
    private authService: AuthService,
    private router: Router,
    private authNotificationService: AuthNotificationService

  ) { }

  ngOnInit() {
  }

  login() {
    this.loading = true;
    this.authService.login(this.model).subscribe((response: ApiResponse<any>) => {
      this.loading = false;
      if (response.status === 1) {
        this.router.navigateByUrl('/');
      }else {
        this.authNotificationService.notify(this.translateService.instant('AUTH.INVALID_LOGIN_OR_PASSWORD'), 'error');
      }
    }, (error: any) => {
      this.loading = false;
      this.authNotificationService.notify(this.translateService.instant('AUTH.SERVER_ERROR'), 'error');
      
    });
  }

}
