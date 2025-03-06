import { ProductSession } from "@components/index";
import { useEffect, useState } from "react";

const TabContent = ({ activeTab }: any) => {
  const [para, setPara] = useState("best-seller");
  useEffect(() => {
    switch (activeTab) {
      case 0:
        setPara("best-seller");
        break;
      case 1:
        setPara("new-arrivlas");
        break;
      case 2:
        setPara("sale");
        break;
    }
  }, [activeTab]);
  return <ProductSession para={para} />;
};

export default TabContent;
