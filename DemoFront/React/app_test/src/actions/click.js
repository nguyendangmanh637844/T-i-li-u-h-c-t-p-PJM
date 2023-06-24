import { CLICK } from "../enums";

export const increase = (payLoad) => {
  return { type: CLICK.Click, payLoad};
};
