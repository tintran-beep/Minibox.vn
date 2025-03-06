import { useState } from "react";
import TabButton from "./TabButton";
import TabContent from "./TabContent";

const Tabs = () => {
  const testtab = ["Best Seller", "New Arrivals", "Sale"];

  const [activeTab, setActiveTab] = useState(0);

  const hanlderSetActiveTab = (index: any) => {
    setActiveTab(index);
  };
  return (
    <>
      <section className="mt-8 mb-4">
        <div className="px-8 mx-auto ">
          <div className="flex justify-center">
            <ul className="grid grid-flow-col text-center gap-10 text-primary">
              <TabButton
                activeTab={activeTab}
                setActiveTab={hanlderSetActiveTab}
                datas={testtab}
              />
            </ul>
          </div>
          <div className="bg-white">
            <div className="mx-auto max-w-2xl px-4 py-6 sm:px-6 sm:py-10 lg:py-8 lg:max-w-7xl lg:px-8">
              <TabContent activeTab={activeTab} />
            </div>
          </div>
        </div>
      </section>
    </>
  );
};

export default Tabs;
