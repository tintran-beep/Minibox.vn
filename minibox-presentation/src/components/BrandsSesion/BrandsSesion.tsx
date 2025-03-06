import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/swiper-bundle.css";
import { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "@stores/hooks";
import { actGetBrands } from "@stores/brands/brandsSlice";
import { TBrand } from "@customtypes/brand";
import { LoadingCard } from "@components/index";

const BrandsSesion = () => {
  const dispatch = useAppDispatch();
  const { records } = useAppSelector((state) => state.brands);
  const [activeTab, setActiveTab] = useState(1);
  const [content, setContentTab] = useState("");

  useEffect(() => {
    if (records && !(records.length > 0)) {
      dispatch(actGetBrands());
    }
  }, [dispatch]);

  useEffect(() => {
    if (records && records.length > 0) {
      setContentTab(records.find((i) => i.id === activeTab)!.content);
    }
  }, [records, activeTab]);

  const handleSlideChange = (e: any) => {
    setActiveTab(e.activeIndex + 1);
  };

  return (
    <section className="mt-4 mb-2 sm:mt-8 ms:mb-8 lg:mt-20 lg:mb-20 text-base md:text-xl lg:text-2xl">
      {records.length > 0 ? (
        <div className="m-auto w-[70%]">
          <div className="w-[80%] text-center font-semibold py-4 px-2 sm:py-8 sm:px-6 lg:py-12 lg:px-8 mx-auto">
            {content}
          </div>
          <Swiper
            slidesPerView={2}
            spaceBetween={10}
            breakpoints={{
              120: {
                slidesPerView: 1,
              },
              544: {
                slidesPerView: 2,
              },
              640: {
                slidesPerView: 4,
              },
              972: {
                slidesPerView: 6,
              },
            }}
            onSlideChange={handleSlideChange}
          >
            {records.map((r: TBrand) => (
              <SwiperSlide key={r.id}>
                <div>
                  <img
                    className={`${
                      activeTab === r.id ? "opacity-100" : "opacity-40"
                    }  mx-auto object-contain w-[150px] h-[82px] text-center hover:opacity-90 cursor-pointer`}
                    src={r.img}
                    onClick={() => setActiveTab(r.id)}
                  />
                </div>
              </SwiperSlide>
            ))}
          </Swiper>
        </div>
      ) : (
        <LoadingCard />
      )}
    </section>
  );
};

export default BrandsSesion;
