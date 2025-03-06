import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import { TBrand } from "@customtypes/brand";

//test
import { brn1, brn2, brn3, brn4, brn5, brn6 } from "@assets/images";

type TResponse = TBrand[];

const actGetBrands = createAsyncThunk(
  "brands/actGetBrands",
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
          content:
            "“1 Millions of combinations, meaning you get a totally unique piece of furniture exactly the way you want it.”",
          img: brn1,
        },
        {
          id: 2,
          content:
            "“2 Great tags, Millie has got used to it, nothing like the old tin tags of years gone by. Light weight and great colours available.”",
          img: brn2,
        },
        {
          id: 3,
          content:
            "“3 Amazing product. The results are so transformative in texture and my face feels plump and healthy. Highly recommend! “",
          img: brn3,
        },
        {
          id: 4,
          content:
            "“4 Millions of combinations, meaning you get a totally unique piece of furniture exactly the way you want it.”",
          img: brn4,
        },
        {
          id: 5,
          content:
            "“5 Great tags, Millie has got used to it, nothing like the old tin tags of years gone by. Light weight and great colours available.”",
          img: brn5,
        },
        {
          id: 6,
          content:
            "“6 Amazing product. The results are so transformative in texture and my face feels plump and healthy. Highly recommend! “",
          img: brn6,
        },
      ];
    } catch (error) {
      if (axios.isAxiosError(error)) {
        return rejectWithValue(error.response?.data.message || error.message);
      } else {
        return rejectWithValue("An unexpected error");
      }
    }
  }
);

export default actGetBrands;
