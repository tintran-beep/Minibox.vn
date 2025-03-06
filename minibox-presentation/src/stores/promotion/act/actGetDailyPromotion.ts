import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import { TDailyPromotion } from "@customtypes/daily-promotion";

//test
import {prBanner1, prBanner2, prBanner3} from "@assets/images";

type TResponse = TDailyPromotion[];

const actGetDailyPromotion = createAsyncThunk(
  "promotions/actGetDailyPromotion",
  async (_, thunkAPI) => {
    const { rejectWithValue } = thunkAPI;
    try {
        
      /*const response = await axios.get<TResponse>(
        "api"
      );
      return response.data;*/

      //fake api
      //await  new Promise(resolve => setTimeout(resolve, 10000000));
        return <TResponse>[
            {
                id: 1,
                title: "Save on Sets",
                type: "Special Offer 1",
                sub: "Made using clean, non-toxic ingredients, our products are designed for everyone",
                discount: 12,
                img : prBanner1,
                deadLine: {
                  years: 2025,
                  month: 3,
                  days: 9,
                  hours: 17,
                  minutes: 32,
                  seconds: 0,
                  miliseconds: 0,
                }
            },
            {
                id: 2,
                title: "Save on Sets",
                type: "Special Offer 2",
                sub: "Made using clean, non-toxic ingredients, our products are designed for everyone",
                discount: 12,
                img : prBanner2,
                deadLine: {
                  years: 2025,
                  month: 3,
                  days: 6,
                  hours: 7,
                  minutes: 32,
                  seconds: 0,
                  miliseconds: 0,
                }
            },
            {
                id: 3,
                title: "Save on Sets",
                type: "Special Offer 3",
                sub: "Made using clean, non-toxic ingredients, our products are designed for everyone",
                discount: 12,
                img : prBanner3,
                deadLine: {
                  years: 2025,
                  month: 3,
                  days: 10,
                  hours: 40,
                  minutes: 20,
                  seconds: 0,
                  miliseconds: 0,
                }
            },
            {
                id: 4,
                title: "Save on Sets",
                type: "Special Offer 4",
                sub: "Made using clean, non-toxic ingredients, our products are designed for everyone",
                discount: 12,
                img : prBanner3,
                deadLine: {
                  years: 2025,
                  month: 3,
                  days: 5,
                  hours: 23,
                  minutes: 33,
                  seconds: 0,
                  miliseconds: 0,
                }
            }
        ] 
    } catch (error) {
      if (axios.isAxiosError(error)) {
        return rejectWithValue(error.response?.data.message || error.message);
      } else {
        return rejectWithValue("An unexpected error");
      }
    }
  }
);

export default actGetDailyPromotion;