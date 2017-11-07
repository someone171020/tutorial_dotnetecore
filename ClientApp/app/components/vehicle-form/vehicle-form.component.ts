import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../../services/vehicle.service';
import { ToastyService } from 'ng2-toasty';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/Observable/forkJoin'

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  models: any[];
  features: any[];
  vehicle: any = {
    features: [],
    contact: {}
  };

  constructor(
    private vehicleService: VehicleService,
    private route: ActivatedRoute,
    private router: Router) {

      route.params.subscribe(p => {
        this.vehicle.id = +p['id'];
      });
    }

  ngOnInit() {
    Observable.forkJoin([
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures(),
      this.vehicleService.getVehicle(this.vehicle.id)
    ]).subscribe(data => {
      this.makes = data[0];
      this.features = data[1];
      this.vehicle = data[2];
    }, err => {
      if (err.status == 404 || err.status == 204)
        this.router.navigate(['/home']);
    });
    // this.vehicleService.getVehicle(this.vehicle.id)
    //   .subscribe(v => {
    //     this.vehicle = v;
    //   }, err => {
    //     if (err.status == 404)
    //       this.router.navigate(['/home']);
    //   });
    // this.vehicleService.getMakes().subscribe(makes => 
    //   this.makes = makes);
    // this.vehicleService.getFeatures().subscribe(features =>
    //   this.features = features);
    
  }

  onMakeChange() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId;
  }

  onFeatureToggle(featureId: any, $event: any) {
    if ($event.target.checked)
      this.vehicle.features.push(featureId);
    else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    this.vehicleService.create(this.vehicle)
      .subscribe(
        x => console.log(x)
        );
  }

}
