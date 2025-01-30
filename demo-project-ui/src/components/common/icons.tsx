import { AiFillDelete } from "react-icons/ai";
import { FaExternalLinkAlt } from "react-icons/fa";
import { RiEdit2Fill } from "react-icons/ri";
import CustomIcon from "./CustomIcon";

const EditIcon = () => {
  return <CustomIcon icon={RiEdit2Fill} label="Edit" />;
};

const DeleteIcon = () => {
  return <CustomIcon icon={AiFillDelete} label="Delete" />;
};

const ExpandIcon = () => {
  return <CustomIcon icon={FaExternalLinkAlt} label="Expand" />;
};

export { DeleteIcon, EditIcon, ExpandIcon };
