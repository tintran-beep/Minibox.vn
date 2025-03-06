const TabButton = ({ datas, activeTab, setActiveTab }: any) => {
  return datas.map((item: string, index: any) => (
    <li
      key={index}
      className={`${
        index === activeTab ?  "text-primary" : "border-transparent text-black"
      } flex justify-center border-t-4 border-primary font-semibold hover:text-primary-brightness hover:border-primary-brightness py-4 cursor-pointer self-center text-base sm:text-2xl lg:text-3xl`}
      onClick={() => setActiveTab(index)}
    >
      {item}
    </li>
  ));
};

export default TabButton;
