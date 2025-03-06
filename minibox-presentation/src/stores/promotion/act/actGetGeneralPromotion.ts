import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import { TGeneralPromotion } from "@customtypes/general-promotion";

//test
import {prBanner1, prBanner2, prBanner3} from "@assets/images";

type TResponse = TGeneralPromotion[];

const actGetGeneralPromotion = createAsyncThunk(
  "promotions/actGetGeneralPromotion",
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
                title: "Autumn Skincare",
                sub: "Discover Now",
                img : prBanner1
            },
            {
                id: 2,
                title: "Anti-aging Cream",
                sub: "Buy 1 Get 1",
                img : prBanner2
            },
            {
                id: 3,
                title: "Autumn Skincare",
                sub: "Discover Now",
                img : prBanner1
            },
            {
                id: 4,
                title: "Sale Up To 40% Off",
                sub: "Shop Sale",
                img : prBanner3
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

export default actGetGeneralPromotion;