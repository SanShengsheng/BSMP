
import { Component, OnInit, Injector, Input, ViewChild, AfterViewInit } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base/modal-component-base';
import { CreateOrUpdateApplicationLogInput,ApplicationLogEditDto, ApplicationLogServiceProxy } from '@shared/service-proxies/service-proxies';
import { Validators, AbstractControl, FormControl } from '@angular/forms';

@Component({
  selector: 'create-or-edit-application-log',
  templateUrl: './create-or-edit-application-log.component.html',
  styleUrls:[
	'create-or-edit-application-log.component.less'
  ],
})

export class CreateOrEditApplicationLogComponent
  extends ModalComponentBase
    implements OnInit {
    /**
    * 编辑时DTO的id
    */
    id: any ;

	  entity: ApplicationLogEditDto=new ApplicationLogEditDto();

    /**
    * 初始化的构造函数
    */
    constructor(
		injector: Injector,
		private _applicationLogService: ApplicationLogServiceProxy
	) {
		super(injector);
    }

    ngOnInit() :void{
		this.init();
    }


    /**
    * 初始化方法
    */
    init(): void {
		this._applicationLogService.getForEdit(this.id).subscribe(result => {
			this.entity = result.applicationLog;
		});
    }

    /**
    * 保存方法,提交form表单
    */
    submitForm(): void {
		const input = new CreateOrUpdateApplicationLogInput();
		input.applicationLog = this.entity;

		this.saving = true;

		this._applicationLogService.createOrUpdate(input)
		.finally(() => (this.saving = false))
		.subscribe(() => {
			this.notify.success(this.l('SavedSuccessfully'));
			this.success(true);
		});
    }

    // =====End===
}
