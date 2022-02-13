import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot({ positionClass: "toast-center-center" }),
    MatDialogModule
  ],
  exports: [
    ToastrModule,
    MatDialogModule
  ]
})
export class SharedModule { }
