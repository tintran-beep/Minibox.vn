import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import { TCategory } from "@customtypes/category";

//test
import {cate1, cate2, cate3, cate4, cate5, cate6} from "@assets/images";

type TResponse = TCategory[];

const actGetCategories = createAsyncThunk(
  "products/actGetCategories",
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
                title: "Moisturizers",
                img : cate1
            },
            {
                id: 2,
                title: "Toners",
                img : cate2
            },
            {
                id: 3,
                title: "Masks",
                img : cate3
            },
            {
                id: 4,
                title: "Kits",
                img : cate4
            },
            {
                id: 5,
                title: "Serum",
                img : cate5
            },
            {
                id: 6,
                title: "Oil Cleansers",
                img : cate6
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

export default actGetCategories;