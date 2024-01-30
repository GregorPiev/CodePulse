import { Component, OnInit } from "@angular/core";

import { AuthService } from "../services/auth.service";
import { CookieService } from "ngx-cookie-service";
import { LoginRequest } from "../models/login-request.model";
import { Router } from "@angular/router";
import { User } from "./../models/user.model";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  model!: LoginRequest;

  constructor(
    private authService: AuthService,
    private cookieService: CookieService,
    private router: Router
  ) {
    this.model = {
      email: "",
      password: "",
    };
  }

  ngOnInit(): void {}

  onFormSubmit(): void {
    console.log(this.model);
    if (!this.model.email && !this.model.password) return;

    this.authService.login(this.model).subscribe({
      next: (response) => {
        console.log("Login response:", response);
        this.cookieService.set(
          "Authorization",
          `Bearer ${response.token}`,
          undefined,
          "/",
          undefined,
          true,
          "Strict"
        );
        this.authService.setUser({
          email: response.email,
          roles: response.roles,
        });
        this.router.navigateByUrl("/");
      },
    });
  }
}
