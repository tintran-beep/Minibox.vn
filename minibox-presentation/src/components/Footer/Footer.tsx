import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPaperPlane } from "@fortawesome/free-solid-svg-icons";
import {
  faFacebook,
  faYoutube,
  faCcPaypal,
  faApplePay,
  faCcMastercard,
  faCcVisa,
  faGooglePay,
} from "@fortawesome/free-brands-svg-icons";
import { NavLink } from "@components/Footer/NavLink";
import { LinkGroup } from "@components/Footer/LinkGroup";
import logo from "@assets/logo_web.svg";
import miniLogo from "@assets/mini_logo.svg";
import { useState } from "react";

const Footer = () => {
  const [email, setEmail] = useState("");

  const hanlderGetEmailContact = () => {
    console.log(email);
  };

  return (
    <>
      <footer className="relative bg-white pb-10 pt-20 lg:pb-20 lg:pt-[80px]">
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-4">
          <div className="lg:col-span-2 self-center px-4">
            <div className="mb-10 w-full mx-auto lg:px-36 flex flex-col gap-4">
              <div>
                <h2 className="text-primary font-bold text-3xl">
                  Care for Your Skin
                </h2>
              </div>
              <div>
                <h2 className="text-primary font-bold text-3xl">
                  Care for Your Beauty
                </h2>
              </div>
              <div>
                <h1 className="text-sx text-gray-500">
                  Give your inbox some love with new products, tips, & more
                </h1>
              </div>
              <div>
                <div>
                  <div className="relative">
                    <div className="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none"></div>
                    <input
                      type="search"
                      id="search"
                      className="block w-full p-4 ps-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-primary-brightness focus:border-primary-brightness"
                      value={email}
                      onChange={(e) => setEmail(e.target.value)}
                      placeholder="Enter your email address"
                      required
                    />
                    <button
                      type="submit"
                      className="text-white absolute end-2.5 bottom-2.5 bg-[#F5F5F5] hover:bg-primary focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm py-2"
                      onClick={hanlderGetEmailContact}
                    >
                      <FontAwesomeIcon
                        className=" text-black w-10 h-10"
                        icon={faPaperPlane}
                      />
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div className="flex justify-around px-4">
            <LinkGroup header="Company">
              <NavLink link="/#" label="About us" />
              <NavLink link="/#" label="Careers" />
              <NavLink link="/#" label="Store Locations" />
              <NavLink link="/#" label="Our Blog" />
              <NavLink link="/#" label="Reviews" />
            </LinkGroup>
            <LinkGroup header="Infomation">
              <NavLink link="/#" label="Start a Return" />
              <NavLink link="/#" label="Contact Us" />
              <NavLink link="/#" label="Shipping FAQ" />
              <NavLink link="/#" label="Terms & Conditions" />
              <NavLink link="/#" label="Privacy Policy" />
            </LinkGroup>
          </div>
        </div>
        <div className="flex flex-col lg:flex-row justify-around items-center m-6 gap-2">
          <div className="flex flex-nowrap gap-2 items-center order-2 lg:order-1">
            <h2 className="text-lg text-gray-600">Minibox.vn</h2>
            <a href="/">
              <FontAwesomeIcon
                className="w-10 h-10 hover:scale-125 transition-all text-blue-500"
                icon={faFacebook}
                size="xl"
              />
            </a>
            <a href="/">
              <FontAwesomeIcon
                className="w-10 h-10 hover:scale-125 transition-all text-red-600"
                icon={faYoutube}
                size="xl"
              />
            </a>
          </div>
          <div className="order-1 lg:order-2">
            <a href="/">
              <img
                className="hidden lg:block sm:hidden object-cover min-h-20 min-w-20"
                src={logo}
              />
              <img
                className="block sm:block lg:hidden object-cover min-h-18 min-w-18"
                src={miniLogo}
              />
            </a>
          </div>
          <div className="h-[100px] flex flex-row gap-4 items-center order-3 lg:order-3">
            <a href="/">
              <FontAwesomeIcon
                icon={faApplePay}
                size="2xl"
                className="text-black"
              />
            </a>
            <a href="/">
              <FontAwesomeIcon
                icon={faGooglePay}
                className="text-black"
                size="2xl"
              />
            </a>
            <a href="/">
              <FontAwesomeIcon
                icon={faCcMastercard}
                size="2xl"
                className="text-black"
              />
            </a>
            <a href="/">
              <FontAwesomeIcon
                icon={faCcPaypal}
                size="2xl"
                className="text-black"
              />
            </a>
            <a href="/">
              <FontAwesomeIcon
                icon={faCcVisa}
                size="2xl"
                className="text-black"
              />
            </a>
          </div>
        </div>
      </footer>
    </>
  );
};

export default Footer;
