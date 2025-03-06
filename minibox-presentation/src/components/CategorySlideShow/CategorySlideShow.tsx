import { TCategory } from "@customtypes/category";
import { CateCard, LoadingCard } from "@components/index";
import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/swiper-bundle.css";
import { Autoplay } from "swiper/modules";
import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "@stores/hooks";
import { actGetCategories } from "@stores/category/categoriesSlice";

const CategorySlideShow = () => {
  const dispatch = useAppDispatch();

  const { records } = useAppSelector((state) => state.categories);

  useEffect(() => {
    if (records && !(records.length > 0)) {
      dispatch(actGetCategories());
    }
  }, [dispatch]);

  return (
    <section className="mt-4 mb-2 sm:mt-8 ms:mb-8 lg:mt-20 lg:mb-20">
      {records.length > 0 ? (
        <div className="px-8 py-8 sm:px-28 lg:px-36">
          <div className="mx-auto text-center">
            <h2 className="text-2xl font-bold text-gray-900 sm:text-3xl lg:text-3xl">
              Our Picks for You
            </h2>
            <p className="mt-4 text-base font-normal leading-7 text-gray-600 lg:text-xl">
              Our products are designed for everyone.
            </p>
          </div>
          <div className="flex justify-center mt-4 lg:mt-8">
            <Swiper
              loop={true}
              autoplay={{
                delay: 1550,
                disableOnInteraction: false,
              }}
              slidesPerView={4}
              spaceBetween={4}
              breakpoints={{
                640: {
                  slidesPerView: 2,
                  spaceBetween: 10,
                },
                768: {
                  slidesPerView: 3,
                  spaceBetween: 20,
                },
                1024: {
                  slidesPerView: 4,
                  spaceBetween: 40,
                },
              }}
              modules={[Autoplay]}
            >
              {records.map((c: TCategory) => (
                <SwiperSlide key={c.id}>
                  <div className="p-2 max-w-[200px] min-w-[72px] m-auto">
                    <CateCard {...c} />
                  </div>
                </SwiperSlide>
              ))}
            </Swiper>
          </div>
        </div>
      ) : (
        <LoadingCard />
      )}
    </section>
  );
};

export default CategorySlideShow;
