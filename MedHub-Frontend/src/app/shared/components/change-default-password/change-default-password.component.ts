import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {AuthenticationService, ChangeDefaultPasswordDto} from "../../services/swagger";

@Component({
  selector: 'app-change-default-password',
  templateUrl: './change-default-password.component.html',
  styleUrl: './change-default-password.component.scss'
})
export class ChangeDefaultPasswordComponent implements OnInit {
  formData: any = {};
  loading: boolean = false;
  userId: number = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
  ) {
  }

  ngOnInit(): void {
    this.getUserId();
  }

  private getUserId() {
    this.route.paramMap.subscribe(params => {
      this.userId = Number(params.get('userId'));
    });
  }

  onSubmit($event: SubmitEvent) {
    $event.preventDefault();

    const changeDefaultPassword: ChangeDefaultPasswordDto = {
      userId: this.userId,
      password: this.formData.password,
      confirmPassword: this.formData.confirmPassword
    }

    this.authenticationService.changeDefaultPassword(changeDefaultPassword).subscribe({
      next: (result) => {
        this.router.navigate(["/login-form"])
      }
    });

  }
}
