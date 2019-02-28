import { Component, OnInit } from '@angular/core';
import { ForgotPasswordModel } from '../../models/forgot-password.model';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { AuthNotificationService } from '../../services/auth-notification.service';
import { ApiResponse } from 'src/app/shared/models/api-response.model';
import { TranslateService } from '@ngx-translate/core';
import { ErrorStateMatcher } from '@angular/material';
import { FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';



class EmailInputErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}


@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  model: ForgotPasswordModel = { email: '' };
  emailIsSent: boolean = false;
  loading: boolean = false;

  emailFormControl = new FormControl(this.model.email, [
    Validators.required,
    Validators.email,
  ]);
  matcher = new EmailInputErrorStateMatcher();

  constructor(
    private router: Router,
    private authService: AuthService,
    private authNotificationService: AuthNotificationService,
    private translateService: TranslateService
  ) { }

  ngOnInit() {
    this.authNotificationService.notify(this.translateService.instant('AUTH.ENTER_EMAIL_NOTIFICATION'), 'info');
  }

  sendEmail() {
    this.loading = true;
    this.model.email = this.emailFormControl.value
    this.authService.forgotPassword(this.model).subscribe((response: ApiResponse<any>) => {
      this.loading = false;
      if (response && response.status === 1) {
        this.emailIsSent = true;
        this.authNotificationService.notify(this.translateService.instant('AUTH.EMAIL_IS_SENT_NOTIFICATION'), 'info');
      } else {
        this.authNotificationService.notify(this.translateService.instant('AUTH.INVALID_EMAIL'), 'error');
      }
    }, (error: any) => {
      this.loading = false;
      this.authNotificationService.notify(this.translateService.instant('AUTH.SERVER_ERROR'), 'error');
    });

  }

  navigateToLoginForm() {
    this.router.navigate(['/auth/login']);
  }
}




