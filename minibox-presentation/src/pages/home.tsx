import {
  HeadingSession,
  CategorySlideShow,
  GeneralPromotion,
  Tabs,
  DailyPromotion,
  BrandsSesion,
  VideoSesion,
  Footer
} from "@components/index";

const Home = () => {

  return (
    <>
      <HeadingSession />
      <CategorySlideShow />
      <GeneralPromotion />
      <Tabs />
      <DailyPromotion />
      <BrandsSesion/>
      <VideoSesion/>
      <Footer/>
    </>
  );
};

export default Home;
