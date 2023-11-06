import { observer } from "mobx-react-lite";
import { Pie } from "./ProgressBar.styles";

const ProgressBar = (props) => {
  return (
      <Pie percentage={props.percents} color={props.color} className="animate" borderWidth="5px">{props.percents}%</Pie>
  );
};

export default observer(ProgressBar);
