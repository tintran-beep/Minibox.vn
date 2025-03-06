import { Outlet } from "react-router-dom";
import {
  MobileNavBar,
  MobileFooter,
} from "@layouts/DefaultLayout/components/NavBar";
import { useState} from "react";
import { ResponsiveMenu } from "@components/index";

const Mobile = () => {
  const [open, setOpen] = useState(false);
  const hanlerClick = () => {
    setOpen(!open);
  };

  return (
    <div className="flex flex-col h-screen">
      <header className="bg-white shadow-xs">
        <MobileNavBar />
      </header>
      <div className="flex-1 overflow-y-auto relative">
        <Outlet />
      </div>
      <ResponsiveMenu open={open} />
      <footer className="py-5 bg-white inset-shadow-2xs">
        <MobileFooter handlerClickMenu={hanlerClick} />
      </footer>
    </div>
  );
};

export default Mobile;
