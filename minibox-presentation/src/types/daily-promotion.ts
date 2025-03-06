import { IDateStartCountDown } from "@interfaces/IDateStartCountDown";

export type TDailyPromotion = {
    id: number;
    title: string;
    type:string;
    discount?:number;
    sub: string;
    img: string;
    deadLine: IDateStartCountDown;
  };
  