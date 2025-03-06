import { Outlet } from "react-router-dom";
import { NavBar, ScrollToTopButton } from "@components/index";

const WebSite = () => {
  return (
    <div className="flex flex-col">
      <div className="fixed z-10">
        <NavBar />
      </div>
      <div className="flex-1 mt-28">
        <Outlet />
      </div>
      <ScrollToTopButton />
    </div>
  );
};

export default WebSite;
