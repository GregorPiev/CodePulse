import { Component, OnInit } from "@angular/core";

import { AuthService } from "src/app/features/auth/services/auth.service";
import { Observable } from "rxjs";
import { Router } from "@angular/router";
import { User } from "src/app/features/auth/models/user.model";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.css"],
})
export class NavbarComponent implements OnInit {
  title = "Observer";
  user?: User;

  constructor(private authServise: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authServise.user().subscribe({
      next: (response) => {
        console.log("Response:", response);
        this.user = response;
      },
    });
    this.user = this.authServise.getUser();
  }

  onLogout(): void {
    this.authServise.logout();
    this.router.navigateByUrl("/");
  }
}
